﻿public enum syntype {
    //toks
    eoftok,       //end of file
    uktok,        //unknown token
    wstok,        //whitespace

    numtok,       //number

    plustok,      //+ (plus symbol)
    minustok,     //- (minus symbol)
    startok,      //* (multiplication symbol)
    slashtok,     //'/' (division symbol)

    oparentok,    //open parenthesis '('
    cparentok,    //closed parenthesis ')'

    //exprs
    litexpr,      //literal expression
    binexpr,      //binary expression
    uniexpr,      //unary expression
    parenexpr     //parenthesis expression
}