using System;
using System.Runtime.Serialization;

namespace CustomSerialization
{
    [Serializable]
    class StringData : ISerializable
    {
        public string dataItemOne;
        public string dataItemTwo;

        public StringData() { }
        public StringData(string one, string two)
        {
            dataItemOne = one;
            dataItemTwo = two;
        }
        protected StringData(SerializationInfo si, StreamingContext ctx)
        {
            // Вызывается автоматически во время десериализации
            // Восстановить переменные-члены из потока
            dataItemOne = si.GetString("First_Item").ToLower();
            dataItemTwo = si.GetString("dataItemTwo").ToLower();
        }
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Вызывается автоматически во время сериализации
            // Наполнить объект SerializationInfo форматированными данными
            info.AddValue("First_Item", dataItemOne.ToUpper());
            info.AddValue("dataItemTwo", dataItemTwo.ToUpper());
        }
    }
}
