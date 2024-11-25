enum syntype {
    eoftok,       //end of file
    numtok,       //number
    wstok,        //whitespace
    plustok,      //+ (plus symbol)
    minustok,     //- (minus symbol)
    startok,      //* (multiplication symbol)
    slashtok,     //'/' (division symbol)
    oparentok,    //open parenthesis '('
    cparentok,    //closed parenthesis ')'
    uktok,        //unknown token

    numexpr,      //number expression
    binexpr,      //binary expression
    parenexpr     //parenthesis expression
}