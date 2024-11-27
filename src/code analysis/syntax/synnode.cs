using System.Reflection;

public abstract class synnode {
    public abstract syntype type { get; }

    public IEnumerable<synnode> getchildren() {
        var props = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in props) {
            if(typeof(synnode).IsAssignableFrom(prop.PropertyType))
                yield return (synnode)prop.GetValue(this);
            else if(typeof(IEnumerable<synnode>).IsAssignableFrom(prop.PropertyType)) { 
                var children = (IEnumerable<synnode>)prop.GetValue(this);
                foreach(var child in children)
                    yield return child;
            }
        }
    }
}