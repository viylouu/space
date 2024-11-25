public enum syntype {
    //toks
    eoftok,       //end of file
    uktok,        //unknown token
    wstok,        //whitespace

    numtok,       //number

    plustok,      //+ (plus symbol)
    minustok,     //- (minus symbol)
    startok,      //* (multiplication symbol)
    slashtok,     //'/' (division symbol)

    bangtok,      //exclamation point '!'

    amptok,       //single ampersand '&'
    bartok,       //single bar '|'

    ampamptok,    //double ampersand
    barbartok,    //double bar

    oparentok,    //open parenthesis '('
    cparentok,    //closed parenthesis ')'

    identtok,     //identity

    //exprs
    litexpr,      //literal expression
    binexpr,      //binary expression
    uniexpr,      //unary expression
    parenexpr,    //parenthesis expression

    //keywords
    truekw,       //true keyword
    falsekw,      //false keyword
    eqeqtok,
    bangeqtok,
}