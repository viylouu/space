internal sealed class bndunioper {
    bndunioper(syntype stype, bnduniopertype type, Type oandcstype) : this(stype, type, oandcstype, oandcstype) {
        this.stype = stype;
        this.type = type;
        this.oandcstype = oandcstype;
        this.rescstype = rescstype;
    }

    bndunioper(syntype stype, bnduniopertype type, Type oandcstype, Type rescstype) {
        this.stype = stype;
        this.type = type;
        this.oandcstype = oandcstype;
        this.rescstype = rescstype;
    }

    public syntype stype { get; }
    public bnduniopertype type { get; }
    public Type oandcstype { get; }
    public Type rescstype { get; }

    static bndunioper[] _opers = {
        new(syntype.bangtok, bnduniopertype.logneg, typeof(bool)),

        new(syntype.minustok, bnduniopertype.neg, typeof(int))
    };

    public static bndunioper bind(syntype stype, Type oandcstype) {
        foreach(var oper in _opers) {
            if(oper.stype == stype && oper.oandcstype == oandcstype)
                return oper;
        }

        return null;
    }
}