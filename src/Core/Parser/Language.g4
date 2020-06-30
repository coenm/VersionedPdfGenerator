grammar Language;

/*------------------------------------------------------------------
 * PARSER RULES
 *------------------------------------------------------------------*/

expression          : (static|myvar)*
                    ;

myvar               : OPEN(myvar2) CLOSE
                    ;

myvar2              : AB(SEP AB)?
                    ;

static              : NAME
                    ;


/*------------------------------------------------------------------
 * LEXER RULES
 *------------------------------------------------------------------*/

fragment LETTER     : [a-zA-Z] ;
fragment DIGIT      : [0-9] ;
fragment LOWERCASE  : [a-z] ;
fragment UPPERCASE  : [A-Z] ;
fragment UNDERSCORE : '_' ;
fragment EXCEPT_END : [^}] ;

OPEN                : '{' ;
CLOSE                : '}' ;
SEP                 : ':' ;

STATIC_TEXT			: (LETTER DIGIT)+ ;

ASTERISK            : '*' ;
SLASH               : '/' ;
PLUS                : '+' ;
MINUS               : '-' ;

ID                  : LETTER DIGIT;
fragment WS         : ' ';
DOT                 : '.';

AB				    : LETTER(LETTER|UNDERSCORE|DIGIT|DOT)+ ;
NAME				: (DIGIT|LETTER|WS|UNDERSCORE|DOT)+ ;

NUMBER              : DIGIT+ ('.' DIGIT+)? ;

// WHITESPACE          : ' ' -> channel(HIDDEN) ;