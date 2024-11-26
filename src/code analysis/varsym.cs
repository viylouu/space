public sealed class varsym {
    internal varsym(string name, Type cstype) {
        this.name = name;
        this.cstype = cstype;
    }

    public string name { get; }
    public Type cstype { get; }
}