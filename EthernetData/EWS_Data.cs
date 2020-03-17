using System;
using System.Collections.Generic;
using System.Text;

namespace ADEng.Library.DMB
{
    /// <summary>
    /// 발령과 응답, byte[]를 구분하기 위한 enum
    /// </summary>
    public enum DType
    {
        ORDER = 1,              //발령
        RESPONSE = 2,           //응답
        GENERAL = 3,            //byte[]
        CONTROLRESPONSE = 4,    //제어 응답
        COMFIRM = 5,            //체크
        ARSRESPONSE = 6,        //ARS 응답
        AMPRESPONSE = 7,        //AMP 응답
        BATTRESPONSE = 8        //배터리 응답
    }

    /// <summary>
    /// serialization과 deserialization로 송수신 하는 클래스
    /// </summary>
    [Serializable]
    public class EWS_Data
    {
        private DateTime dateTime;
        private byte typeCode;
        private object objClass;

        /// <summary>
        /// 데이터를 전송한 시간
        /// </summary>
        public DateTime DDateTime
        {
            get { return this.dateTime; }
            set { this.dateTime = value; }
        }

        /// <summary>
        /// 데이터 구분 코드
        /// 1 - 발령
        /// 2 - 응답
        /// 3 - byte[]
        /// </summary>
        public byte TypeCode
        {
            get { return this.typeCode; }
            set { this.typeCode = value; }
        }

        /// <summary>
        /// 실제 데이터 클래스
        /// OrderData 또는 ResponseData 또는 byte[] 로 캐스팅하여 사용한다.
        /// </summary>
        public object ObjClass
        {
            get { return this.objClass; }
            set { this.objClass = value; }
        }

        /// <summary>
        /// 기본생성자
        /// </summary>
        public EWS_Data()
        {
        }

        /// <summary>
        /// 기본생성자
        /// </summary>
        /// <param name="_dt">
        /// 데이터를 전송한 시간
        /// </param>
        /// <param name="_type">
        /// 데이터 구분 코드
        /// 1 - 발령
        /// 2 - 응답
        /// 3 - byte[]
        /// </param>
        /// <param name="_class">
        /// 실제 데이터 클래스
        /// OrderData 또는 ResponseData 또는 byte[] 로 캐스팅하여 사용한다.
        /// </param>
        public EWS_Data(DateTime _dt, byte _type, object _class)
        {
            this.dateTime = _dt;
            this.typeCode = _type;
            this.objClass = _class;
        }
    }
}