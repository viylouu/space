internal sealed class bndbinoper {
    bndbinoper(syntype stype, bndbinopertype type, Type cstype) : this(stype, type, cstype, cstype, cstype) { }

    bndbinoper(syntype stype, bndbinopertype type, Type leftcstype, Type rightcstype, Type rescstype) {
        this.stype = stype;
        this.type = type;
        this.leftcstype = leftcstype;
        this.rightcstype = rightcstype;
        this.rescstype = rescstype;
    }

    public syntype stype { get; }
    public bndbinopertype type { get; }
    public Type leftcstype { get; }
    public Type rightcstype { get; }
    public Type rescstype { get; }

    static bndbinoper[] _opers = {
        new(syntype.plustok, bndbinopertype.add, typeof(int)),
        new(syntype.minustok, bndbinopertype.sub, typeof(int)),
        new(syntype.startok, bndbinopertype.mul, typeof(int)),
        new(syntype.slashtok, bndbinopertype.div, typeof(int)),

        new(syntype.ampamptok, bndbinopertype.logand, typeof(bool)),
        new(syntype.barbartok, bndbinopertype.logor, typeof(bool)),
    };

    public static bndbinoper bind(syntype stype, Type leftcstype, Type rightcstype) {
        foreach(var oper in _opers) {
            if(oper.stype == stype && oper.leftcstype == leftcstype && oper.rightcstype == rightcstype)
                return oper;
        }

        return null;
    }
}