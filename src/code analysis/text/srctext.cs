using System.Collections.Immutable;

public sealed class srctext {
    public ImmutableArray<textline> lines { get; }
    readonly string _text;

    srctext(string text) {
        _text = text;
        lines = parselines(this, text);
    }

    public int getlineidx(int pos) {
        var lower = 0;
        var upper = lines.Length - 1;

        while(lower <= upper) {
            var idx = lower + (upper - lower) / 2;
            var start = lines[idx].start;

            if(pos == start)
                return idx;

            if(start > pos) {
                upper = idx - 1;
            } else {
                lower = idx + 1;
            }
        }

        return lower - 1;
    }

    ImmutableArray<textline> parselines(srctext srctxt, string text) {
        var res = ImmutableArray.CreateBuilder<textline>();

        var pos = 0;
        var linestart = 0;

        while(pos < text.Length) {
            var lbw = getlinebreakwidth(text, pos);

            if(lbw == 0)
                pos++;
            else {
                addline(res, srctxt, pos, linestart, lbw);

                pos += lbw;
                linestart = pos;
            }
        }

        if(pos >= linestart)
            addline(res, srctxt, pos, linestart, 0);

        return res.ToImmutable();
    }

    public char this[int i] => _text[i];

    public int Length => _text.Length;

    private static void addline(ImmutableArray<textline>.Builder res, srctext srctxt, int pos, int linestart, int lbw) {
        var linelen = pos - linestart;
        var linelenwlinebreak = linelen + lbw;
        var line = new textline(srctxt, linestart, linelen, linelenwlinebreak);
        res.Add(line);
    }

    static int getlinebreakwidth(string text, int i) {
        var c = text[i];
        var l = i >= text.Length-1 ? '\0' : text[i + 1];

        if(c == '\r' && l == '\n')
            return 2;

        if(c == '\r' || c == '\n')
            return 1;

        return 0;
    }

    public static srctext from(string text) { 
        return new srctext(text);
    }

    public override string ToString() => _text;

    public string ToString(int start, int len) => _text.Substring(start, len);

    public string ToString(textspan span) => ToString(span.start, span.len);
}