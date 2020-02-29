using System;
using System.Runtime.Serialization;

namespace CustomSerialization
{
    [Serializable]
    class MoreData
    {
        public string dataItemOne;
        public string dataItemTwo;

        public MoreData() { }
        public MoreData(string one, string two)
        {
            dataItemOne = one;
            dataItemTwo = two;
        }

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            // Вызывается автоматически во время сериализации
            dataItemOne = dataItemOne.ToUpper();
            dataItemTwo = dataItemTwo.ToUpper();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            // Вызывается автоматически по завершении десериализации
            dataItemOne = dataItemOne.ToLower();
            dataItemTwo = dataItemTwo.ToLower();
        }
    }
}
