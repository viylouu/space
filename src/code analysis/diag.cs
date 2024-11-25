public sealed class diag {
    public diag(txtspan span, string msg) {
        this.span = span;
        this.msg = msg;
    }

    public txtspan span { get; }
    public string msg { get; }

    public override string ToString() => msg;
}