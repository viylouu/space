public static class synfacts {
    public static int getbinoperprec(this syntype type) {
        switch(type) {
            case syntype.startok:
            case syntype.slashtok:
                return 5;

            case syntype.plustok:
            case syntype.minustok:
                return 4;

            case syntype.eqeqtok:
            case syntype.bangeqtok:
                return 3;

            case syntype.ampamptok:
                return 2;

            case syntype.barbartok:
                return 1;

            default:
                return 0;
        }
    }

    public static IEnumerable<syntype> getbinopertypes() {
        var types = (syntype[]) Enum.GetValues(typeof(syntype));
        foreach(var t in types)
            if(getbinoperprec(t) > 0)
                yield return t;
    }

    public static IEnumerable<syntype> getuniopertypes() {
        var types = (syntype[])Enum.GetValues(typeof(syntype));
        foreach(var t in types)
            if(getunioperprec(t) > 0)
                yield return t;
    }

    public static int getunioperprec(this syntype type) {
        switch(type) {
            case syntype.bangtok:
            case syntype.minustok:
                return 6;

            default:
                return 0;
        }
    }

    public static syntype getkwtype(string text) {
        switch(text) {
            case "true":
                return syntype.truekw;
            case "false":
                return syntype.falsekw;
            default:
                return syntype.identtok;
        }
    }

    public static string gettext(syntype type) {
        switch(type) {
            case syntype.plustok:
                return "+";
            case syntype.minustok:
                return "-";
            case syntype.startok:
                return "*";
            case syntype.slashtok:
                return "/";
            case syntype.bangtok:
                return "!";
            case syntype.amptok:
                return "&";
            case syntype.bartok:
                return "|";
            case syntype.ampamptok:
                return "&&";
            case syntype.barbartok:
                return "||";
            case syntype.eqeqtok:
                return "==";
            case syntype.bangeqtok:
                return "!=";
            case syntype.colgreattok:
                return ":>";
            case syntype.coltok:
                return ":";
            case syntype.colcoltok:
                return "::";
            case syntype.oparentok:
                return "(";
            case syntype.cparentok:
                return ")";
            case syntype.truekw:
                return "true";
            case syntype.falsekw:
                return "false";
            default:
                return null;
        }
    }
}