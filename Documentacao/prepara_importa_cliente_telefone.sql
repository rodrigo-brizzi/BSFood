declare @tbClienteNovo table(
	[cli_codigoNovo] [int] IDENTITY(1,1) NOT NULL,
	[cli_codigo] [int] NOT NULL)

insert into @tbClienteNovo 
select
[CLI_CODIGO]--cli_codigo
from CLIENTES 
order by [CLI_CODIGO]

select
'insert into tbClienteTelefone values (''' as a,
REPLACE([CTL_NUMERO],'-',''),--ctl_numero
''',' as b,
(select [cli_codigoNovo] from @tbClienteNovo where [cli_codigo] = CLIENTE_TELEFONE.CLI_CODIGO),--cli_codigo
')' as c
from CLIENTE_TELEFONE
order by [CLI_CODIGO]
