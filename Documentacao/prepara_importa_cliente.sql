select
'insert into tbClienteGrupo values (''' as a,
[CG_NOME],--cgr_nome
''',' as b,
[CG_DIA_FECHAMENTO],--cgr_fechamento
',' as c,
[CG_DIA_VENCIMENTO],--cgr_vencimento
')' as d
from CLIENTE_GRUPO
order by [CG_CODIGO]

select
'insert into tbCliente values (''' as a,
[CLI_NOME],--cli_nome
''',''' as b,
'',--cli_nomeFantasia
''',''' as c,
'F',--cli_tipo
''',''' as d,
ISNULL([CLI_CPF],''),--cli_cpfCnpj
''',''' as e,
ISNULL([CLI_RG],''),--cli_rgIe
''',''' as f,
CASE WHEN ISNULL([CLI_SEXO], 'M') <> 'M' and ISNULL([CLI_SEXO], 'M') <> 'F' THEN 'M' ELSE ISNULL([CLI_SEXO], 'M') END as [CLI_SEXO],--cli_sexo
''',''' as g,
ISNULL([CLI_EMAIL],''),--cli_email
''',''' as h,
ISNULL([CLI_OBS],''),--cli_observacao
''',' as i,
ISNULL([CLI_DIA_FECHAMENTO],0),--cli_fechamento
',' as j,
ISNULL([CLI_DIA_VENCIMENTO],0),--cli_vencimento
',' as h,
ISNULL([CLI_LIMITE], 0),--cli_limiteCredito
',' as l,
NULL,--cli_dataNascimento
',' as m,
[CG_CODIGO],--cgr_codigo
')--' as n,
ROW_NUMBER() OVER (ORDER BY [CLI_CODIGO])  AS Codigo_novo
from CLIENTES 
order by [CLI_CODIGO]