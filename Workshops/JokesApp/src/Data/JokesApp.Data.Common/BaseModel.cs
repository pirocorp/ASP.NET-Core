namespace JokesApp.Data.Common
{
    using System;

    /// <summary>
    /// Generic BaseModel with id of type T
    /// </summary>
    /// <typeparam name="T">Type of Id</typeparam>
    public abstract class BaseModel<T>
    {
        public T Id { get; set; }
    }

    /// <summary>
    /// Default BaseModel with string id
    /// </summary>
    public abstract class BaseModel : BaseModel<string>
    {
        protected BaseModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
