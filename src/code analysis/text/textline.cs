public sealed class textline {
    public textline(srctext text, int start, int len, int lenwlinebreak) {
        this.text = text;
        this.start = start;
        this.len = len;
        this.lenwlinebreak = lenwlinebreak;
    }

    public srctext text { get; }
    public int start { get; }
    public int len { get; }
    public int end => start + len;
    public int lenwlinebreak { get; }
    public textspan span => new textspan(start, len);
    public textspan spanwlinebreak => new textspan(start, lenwlinebreak);
    public override string ToString() => text.ToString(span);
}