internal static class synfacts {
    public static int getbinoperprec(this syntype type) {
        switch(type) {
            case syntype.startok:
            case syntype.slashtok:
                return 4;

            case syntype.plustok:
            case syntype.minustok:
                return 3;

            case syntype.ampamptok:
                return 2;

            case syntype.barbartok:
                return 1;

            default:
                return 0;
        }
    }

    public static int getunioperprec(this syntype type) {
        switch(type) {
            case syntype.bangtok:
            case syntype.minustok:
                return 5;

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
}