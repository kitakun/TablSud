using System;
using MongoDB.Bson.Serialization.Attributes;

namespace TablSud.Core.Domain.Court
{
    /// <summary>
    /// Элемент таблицы о судах
    /// </summary>
    public class ConvictionsItem : MongoEntity
    {
        /// <summary>
        /// Наименование суда
        /// </summary>
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string CourtName { get; set; }
        /// <summary>
        /// Номер дела
        /// </summary>
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string CaseNumber { get; set; }

        /// <summary>
        /// Стороны
        /// </summary>
        [BsonElement]
        public ModelConvictionSides Sides { get; set; }

        /// <summary>
        /// Требования
        /// </summary>
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Requirements { get; set; }

        /// <summary>
        /// Прогресс дела
        /// </summary>
        [BsonElement]
        public ModelConvictionProgress Progress { get; set; }

        /// <summary>
        /// Результат рассмотрения дела
        /// </summary>
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string ConsiderationResult { get; set; }

        /// <summary>
        /// Сведения об исполнении
        /// </summary>
        [BsonElement]
        public string PerformanceInformation { get; set; }
    }

    /// <summary>
    /// Стороны суда
    /// </summary>
    public class ModelConvictionSides
    {
        /// <summary>
        /// Истец
        /// </summary>
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string SidePlaintiff { get; set; }

        /// <summary>
        /// Ответчик
        /// </summary>
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string SideDefendant { get; set; }

        /// <summary>
        /// Третьи лица
        /// </summary>
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string SideThirdPartyMember { get; set; }
    }

    /// <summary>
    /// Информация о движении дела
    /// </summary>
    public class ModelConvictionProgress
    {
        /// <summary>
        /// Процессуальные действия и стадии судебного производства
        /// </summary>
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string ActionsOrStages { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime Date { get; set; }

        public string ShortDate => Date.ToString("dd.MM.yyyy");
    }
}
