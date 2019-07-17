/* Companion source code for "flex & bison", published by O'Reilly
 * Media, ISBN 978-0-596-15597-1
 * Copyright (c) 2009, Taughannock Networks. All rights reserved.
 * See the README file for license conditions and contact info.
 * $Header: /home/johnl/flnb/code/RCS/fb3-2.y,v 2.1 2009/11/08 02:53:18 johnl Exp $
 */
/* calculator with AST */

%{
#  include <stdio.h>
#  include <stdlib.h>
#  include <math.h>
#  include "fb3-3.h"
extern FILE *yyout;
%}

%union {
  struct ast *a;
  double d;
  struct symbol *s;		/* which symbol */
  struct symlist *sl;
  int fn;			/* which function */
}

/* declare tokens */
%token <d> NUMBER
%token <s> NAME
%token <fn> FUNC
%token EOL

%token IF THEN ELSE WHILE DO LET

%nonassoc Lower_e
%nonassoc <fn> CMP
%right '='
%left '+' '-'
%left '*' '/'
%nonassoc '|' UMINUS

%nonassoc Lower
%left '('
%nonassoc ';'


%type <a> exp compound_statement block_item_list expression_statement selection_statement iteration_statement statement explist
%type <sl> symlist

%start calclist

%nonassoc LOWER_THAN_ELSE
%nonassoc ELSE
%%

exp: exp CMP exp          { $$ = newcmp($2, $1, $3); }
   | exp '+' exp          { $$ = newast('+', $1,$3); }
   | exp '-' exp          { $$ = newast('-', $1,$3);}
   | exp '*' exp          { $$ = newast('*', $1,$3); }
   | exp '/' exp          { $$ = newast('/', $1,$3); }
   | '|' exp              { $$ = newast('|', $2, NULL); }
   | '(' exp ')'          { $$ = $2; }
   | '-' exp %prec UMINUS { $$ = newast('M', $2, NULL); }
   | NUMBER               { $$ = newnum($1); }
   | FUNC '(' explist ')' { $$ = newfunc($1, $3); }
   | NAME    %prec Lower  { $$ = newref($1); }
   | NAME '=' exp         { $$ = newasgn($1, $3); }
   | NAME '(' explist ')' { $$ = newcall($1, $3); }
;


explist: exp
 | exp ',' explist  { $$ = newast('L', $1, $3); }
;

symlist: NAME       { $$ = newsymlist($1, NULL); }
 | NAME ',' symlist { $$ = newsymlist($1, $3); }
;

compound_statement: '{' '}' { $$ = NULL; }
                  | '{' block_item_list '}' { $$ = $2; }
;

block_item_list: statement { $$ = $1; }
               | block_item_list statement { $$ = newast('L', $1, $2); }
;

expression_statement: ';' { $$ = NULL; }
                    | exp ';' { $$ = $1; }
;

selection_statement: 
  IF exp THEN statement %prec LOWER_THAN_ELSE { $$ = newflow('I', $2, $4, NULL); }
  | IF exp THEN statement ELSE statement { $$ = newflow('I', $2, $4, $6); }
;

iteration_statement: 
  WHILE exp DO statement { $$ = newflow('W', $2, $4, NULL); }
;

statement: compound_statement
         | expression_statement
         | selection_statement
         | iteration_statement
         | exp %prec Lower_e
;

calclist: /* nothing */
  | calclist statement EOL {
    if(debug) dumpast($2, 0);
    else{
      char ch[100];
      //gcvt(eval($2),10,ch);
      //printf(ch);

      sprintf(ch, "%g", eval($2));

     fprintf(yyout,"%s\n", ch);
     treefree($2);
    }
  }
  | calclist LET NAME '(' symlist ')' compound_statement EOL {
                       dodef($3, $5, $7);
                       fprintf(yyout,"Defined %s\n", $3->name); }

  | calclist error EOL { yyerrok; fprintf(yyout,""); }
 ;
%%
