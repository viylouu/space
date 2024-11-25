public struct txtspan {
    public txtspan(int start, int len) {
        this.start = start;
        this.len = len;
    }

    public int start { get; }
    public int len { get; }
    public int end => start + len;
}