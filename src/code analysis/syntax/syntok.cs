public sealed class syntok : synnode {
    public syntok(syntype type, int pos, string text, object val) {
        this.type = type;
        this.pos = pos;
        this.text = text;
        this.val = val;
    }

    public override syntype type { get; }

    public int pos { get; }
    public string text { get; }
    public object val { get; }
    public override textspan span => new(pos, text?.Length ?? 0);
}