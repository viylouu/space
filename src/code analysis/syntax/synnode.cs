using System.Diagnostics.Tracing;
using System.Reflection;

public abstract class synnode {
    public abstract syntype type { get; }

    public virtual textspan span {
        get {
            var first = getchildren().First().span;
            var last = getchildren().Last().span;
            return textspan.frombounds(first.start, last.end);
        }
    }

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

    public void writeto(TextWriter writer) {
        prettyprint(writer, this);
    }

    static void prettyprint(TextWriter writer, synnode node, string indent = "", bool isLast = true) {
        var marker = isLast ? "└──" : "├──";

        writer.Write(indent);
        writer.Write(marker);
        writer.Write(node.type);

        if(node is syntok t && t.val != null) {
            writer.Write(" ");
            writer.Write(t.val);
        }

        writer.WriteLine();

        var lastchild = node.getchildren().LastOrDefault();

        indent += isLast?"   ":"│  ";

        foreach(var child in node.getchildren())
            prettyprint(writer, child, indent, child==lastchild);
    }

    public override string ToString() {
        using(var writer = new StringWriter()) {
            writeto(writer);

            return writer.ToString();
        }
    }
}