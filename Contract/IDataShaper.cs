using System.Dynamic;

namespace Contract
{
    public interface IDataShaper<in T>
    {
        IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string? fieldsString);
        ExpandoObject ShapeData(T entity, string? fieldsString);
    }
}
