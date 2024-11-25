class syntok : synnode {
    public syntok(syntype type, int pos, string txt, object val) {
        this.type = type;
        this.pos = pos;
        this.txt = txt;
        this.val = val;
    }

    public override syntype type { get; }

    public int pos { get; }
    public string txt { get; }
    public object val { get; }

    public override IEnumerable<synnode> getchildren() {
        return Enumerable.Empty<synnode>();
    }
}