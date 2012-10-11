namespace ProjectCore.Model
{
    public class EntityDataSourceModel
    {
        private string _where;
        private string _orderBy;
        private string _entitySetName;
        private string _defaultContainerName;
        private string _connectionString;

        /// <summary>
        /// 
        /// </summary>
        public string Where
        {
            set { _where = value; }
            get { return _where; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OrderBy
        {
            set { _orderBy = value; }
            get { return _orderBy; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EntitySetName
        {
            set { _entitySetName = value; }
            get { return _entitySetName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DefaultContainerName
        {
            set { _defaultContainerName = value; }
            get { return _defaultContainerName; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString
        {
            set { _connectionString = value; }
            get { return _connectionString; }
        }
    }
}
