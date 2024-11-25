internal static class synfacts {
    public static int getbinoperprec(this syntype type) {
        switch(type) {
            case syntype.plustok:
            case syntype.minustok:
                return 1;

            case syntype.startok:
            case syntype.slashtok:
                return 2;

            default:
                return 0;
        }
    }
}