grammar Language;

/*------------------------------------------------------------------
 * PARSER RULES
 *------------------------------------------------------------------*/
expression          : ( text | variable )*
                    ;

variable            : '{' var=KEY (':' arg=text)? '}'
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

KEY                 : LETTER(LETTER|DIGIT|UNDERSCORE|DOT|MINUS)+ ;
TEXT                :       (LETTER|DIGIT|UNDERSCORE|DOT|MINUS|WS|SLASH)+;
NEWLINE             : ('\r'? '\n' | '\r') -> channel(HIDDEN) ;
WHITESPACE          : (' ' | '\t' ) -> channel(HIDDEN) ;