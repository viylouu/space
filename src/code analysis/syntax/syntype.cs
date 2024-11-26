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
    eqeqtok,      //double equals
    bangeqtok,    //not equals

    colgreattok,  //colon greater than token ':>'
    coltok,       //colon token ':'
    colcoltok,    //colon colon token '::'

    oparentok,    //open parenthesis '('
    cparentok,    //closed parenthesis ')'

    identtok,     //identity

    //exprs
    litexpr,      //literal expression
    binexpr,      //binary expression
    uniexpr,      //unary expression
    parenexpr,    //parenthesis expression
    nameexpr,     //name expression
    assignexpr,   //assignment expression

    //keywords
    truekw,       //true keyword
    falsekw,      //false keyword
}