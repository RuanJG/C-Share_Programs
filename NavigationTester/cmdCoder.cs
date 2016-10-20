using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NavigationTester
{
    class cmdCoder
    {

        private int CMD_CODER_MAX_DATA_LEN = 128;
        // tag[2]+id[1]+len[1~4]+data[len~2*len]+crc[1]
        private int CMD_CODER_MAX_PACKGET_LEN = (2+1+4+128*2+1); //(2+1+4+CMD_CODER_MAX_DATA_LEN*2+1);
        private byte CMD_CODER_TAG = 0xff;

        public delegate int encodeSendCallback(byte c);
        private encodeSendCallback CMD_CODER_CALL_BACK_NULL = null;


	    byte FIND_TAG =0,
	        FIND_ID=1,
	        FIND_LEN=2,
	        FIND_DATA=3,
	        FIND_CRC=4,
	        FIND_DONE=5;


//encodeSendCallback , send encoded byte in encode function, if fail return 0 ,else 1
        
//typedef struct _cmdcoder_t{

	        public UInt32 len=0; //default = 0
	        public byte[] data = new byte[128]; //CMD_CODER_MAX_DATA_LEN
	        public byte  id;
	        public byte  sum_crc; // defult = 0
	        //for parse
	        byte parse_status; // defalut= CmdCoderStep::FIND_TAG
	        byte last_byte_is_tag; // defalt=0
	        UInt32 len_multi ; // defalut = 1 , step 128
	        UInt32 index; //default = 0, <PROTOCOL_MAX_DATA_LEN ;
	        encodeSendCallback send_cb;
//}cmdcoder_t;



            public cmdCoder(byte set_id, encodeSendCallback sendCallback)
            {
                cmdcoder_init(set_id, sendCallback);
            }

        private int encode_variable_len(UInt32 value, ref byte[] buff , int size) {
	        int offset = 0;
            //byte[] data= new byte[size];
	        do {
		        //if (offset >= size) {
		        // event 0 data , encode len too
		        if (offset > size) {
			        return -1;
		        }
		        buff[offset] = Convert.ToByte( value%128 );
		        value = value / 128;

		        if (value > 0) {
			        buff[offset] |= 128;
		        }

		        ++offset;
	        } while (value > 0);

	        return offset;
        }

        void cmdcoder_init(byte set_id, encodeSendCallback sendCallback)
        {
	        parse_status = FIND_TAG;
	        last_byte_is_tag = 0;
	        len_multi = 1;
	        this.len = 0;
	        index = 0;
	        sum_crc = 0;
	        send_cb = sendCallback;
	        id = set_id;
        }

        public int DecodeErrorCount = 0;
        public int DecodeIgnoreByteCount = 0;
        public int cmdcoder_Parse_byte(byte pbyte )
        {
	        byte  decode_a_packget = 0;
	        byte decode_failed = 0;
	
            if( parse_status == 5){
                cmdcoder_init(id, send_cb);
            }
	        switch( parse_status ){

		        case 0:{
			        if( pbyte != CMD_CODER_TAG ){
				        last_byte_is_tag=0;
                        if( DecodeIgnoreByteCount < int.MaxValue ) DecodeIgnoreByteCount++;
			        }else {
				        if( last_byte_is_tag == 1 ){
					        parse_status++; // found tag,next step
					        last_byte_is_tag = 0; //id no need to check tag last byte
				        }else{
					        last_byte_is_tag=1; //find next 0xff next time
				        }
			        }
			        break;
		        }
		
		        case 1:{
			        if(  last_byte_is_tag == 1 ){
				        //ignore this byte or find a error
				        if( pbyte == 0x00 ){
					         last_byte_is_tag = 0;
				        }else{
					        decode_failed = 1;
				        }
				        break;
			        }else if( pbyte == CMD_CODER_TAG){
				         last_byte_is_tag=1;
			        }
			
			        //add for check sum
			         sum_crc += pbyte;
			
			        //use this byte for id
			         id = pbyte;
			         parse_status++;
			        break;
		        }
		
		        case 2:{
			        if(  last_byte_is_tag == 1 ){
				        //ignore this byte or find a error
				        if( pbyte == 0x00 ){
					         last_byte_is_tag = 0;
				        }else{
					        decode_failed = 1;
				        }
				        break;
			        }else if( pbyte == CMD_CODER_TAG){
				         last_byte_is_tag=1;
			        }
			
			        //add for check sum
			         sum_crc += pbyte; 
			
			        //decode len
			         len += Convert.ToUInt32((pbyte & 0x7f)*  len_multi );
			        if( (pbyte & 0x80) == 0 ){
				        //len decoce finish
				         parse_status++;
				        if(  len == 0 )
					         parse_status++; //ignore data collect
			        }else{
				         len_multi *= 128;
			        }
			        if(  len > CMD_CODER_MAX_DATA_LEN ){
				        //check len for too big or data error (¼ì²é³¤¶È£¬ºÍ£¬·ÀÖ¹Êý¾Ý´«ÊäÊ±±»ÐÞ¸ÄµÄ¿ÉÄÜ)
				        decode_failed=1;
			        }
			        break;
		        }
		
		        case 3:{
			        if(  last_byte_is_tag == 1 ){
				        //ignore this byte or find a error
				        if( pbyte == 0x00 ){
					         last_byte_is_tag = 0;
				        }else{
					        decode_failed = 1;
				        }
				        break;
			        }else if( pbyte == CMD_CODER_TAG){
				        last_byte_is_tag=1;
			        }
			
			        //add for check sum
			        sum_crc += pbyte;
			
			        //fill in buff
			        data[index++]=pbyte;
			        if( index >= len ){
				        parse_status++; //ok ,next to crc
				        index=0; //for crc
			        }
			        break;
		        }
		
		        case 4:{
			        if( last_byte_is_tag == 1 ){
				        //ignore this byte or find a error
				        if( pbyte == 0x00 ){
					        last_byte_is_tag = 0;
				        }else{
					        decode_failed = 1;
				        }
				        break;
			        }else if( pbyte == CMD_CODER_TAG){
				        last_byte_is_tag=1;
			        }
			
			        // check crc
			        if( pbyte != sum_crc ){
				        decode_failed = 1;
			        }else{
				        decode_a_packget = 1;
				        parse_status++; //ok ,next packget
			        }
			        break;
		        }
	        }
	
	        if( decode_failed == 1 ){
		        cmdcoder_init(id,send_cb);
                if (DecodeErrorCount < int.MaxValue) DecodeErrorCount++;
	        }
	
	        return decode_a_packget;
        }


       int count = 0;
       void Check_and_Send(byte b) {
	        
		         send_cb(b); 
		        count++; 
		        if( b == CMD_CODER_TAG){ 
			         send_cb(0x00); 
			        count++; 
		        } 
       }

        //encode packget->data&&packget->id,  and send it by packget->send_cb
        //return count of bytes in encode frame;  if count < 0 error;  if count ==0 send_callback not set; 
        int cmdcoder_encode_and_send(){
	        int offset=0;
	        int i;
	        byte[] sendbuff = new byte[8];//len buff , it can not bigger than 8byte
	        byte sum=0;
	        
	
	        if( send_cb == CMD_CODER_CALL_BACK_NULL )
		        return 0;
	
            count = 0;
	
	         send_cb(CMD_CODER_TAG);
	         send_cb(CMD_CODER_TAG);
	        count+=2;

	        //id
	        sum +=  id;
	        Check_and_Send( id);
	        //len
	        offset = encode_variable_len( len, ref sendbuff,8);
	        if( offset <= 0 ) 
		        return -1;
	        for( i=0; i< offset; i++){
		        Check_and_Send(sendbuff[i]);
		        sum += sendbuff[i];
	        }
	        //data
	        for( i=0; i<  len; i++){
		        Check_and_Send( data[i]);
		        sum +=  data[i];
	        }
	        //crc
	        Check_and_Send(sum);
	        //packget->sum_crc = sum;
	
	        return count;
        }

        public void cmdcoder_send_bytes(byte []buff, int size)
        {
	        int i;
	
	        //packget.id = id;
	         len = 0;
	        for( i=0; i<  size; i++)
	        {
		         data[i] = buff[i];
		         len++;
		        if(  len >= CMD_CODER_MAX_DATA_LEN ){
			        cmdcoder_encode_and_send( );
			         len = 0;
		        }
	        }
	        if(  len > 0 ){
			        cmdcoder_encode_and_send(  );
			         len = 0;
	        }
        }


















    }
}
