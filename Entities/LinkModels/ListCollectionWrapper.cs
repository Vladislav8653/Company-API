using System.Xml;

namespace Entities.LinkModels;

public class LinkCollectionWrapper<T> : LinkResourseBase
{
    public List<T> Value { get; set; } = new List<T>();

    public LinkCollectionWrapper()
    { }
    
    public LinkCollectionWrapper(List<T> value)
    {
        Value = value;
    }
}