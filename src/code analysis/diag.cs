public sealed class diag {
    public diag(textspan span, string msg) {
        this.span = span;
        this.msg = msg;
    }

    public textspan span { get; }
    public string msg { get; }

    public override string ToString() => msg;
}