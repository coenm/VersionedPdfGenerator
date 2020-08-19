grammar Language;

/*------------------------------------------------------------------
 * PARSER RULES
 *------------------------------------------------------------------*/
expression          : ( text | variable | function )*
                    ;

variable            : '{' var=KEY (':' arg=text)? '}'
                    ;

function            : '{' func=KEY '(' arg=expression ')' '}'
                    ;

text                : ( TEXT | KEY | ':' )+
                    ;

/*------------------------------------------------------------------
 * LEXER RULES
 *------------------------------------------------------------------*/
fragment LETTER     : [a-zA-Z] ;
fragment DIGIT      : [0-9] ;
fragment UNDERSCORE : '_' ;
fragment WS         : ' ' ;
fragment DOT        : '.' ;
fragment MINUS      : '-' ;
fragment SEMICOLUMN : ':' ;
fragment SLASH      : '/' ;
fragment PERCENT    : '%' ;

KEY                 : LETTER(LETTER|DIGIT|UNDERSCORE|DOT|MINUS)+ ;
TEXT                :       (LETTER|DIGIT|UNDERSCORE|DOT|MINUS|WS|SLASH|PERCENT)+;
NEWLINE             : ('\r'? '\n' | '\r') -> channel(HIDDEN) ;
WHITESPACE          : (' ' | '\t' ) -> channel(HIDDEN) ;