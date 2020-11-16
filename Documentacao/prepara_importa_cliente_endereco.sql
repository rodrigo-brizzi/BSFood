declare @tbClienteNovo table(
	[cli_codigoNovo] [int] IDENTITY(1,1) NOT NULL,
	[cli_codigo] [int] NOT NULL)

declare @tbBairroNovo table(
	[bai_codigoNovo] [int] IDENTITY(1,1) NOT NULL,
	[bai_codigo] [int] NOT NULL)

insert into @tbClienteNovo 
select
[CLI_CODIGO]--cli_codigo
from CLIENTES 
order by [CLI_CODIGO]

insert into @tbBairroNovo
select 
[BAI_CODIGO]--bai_codigo
from BAIRROS 
order by [BAI_CODIGO]

select 
'insert into tbClienteEndereco values (''' as a,
CLI_ENDERECO,--cen_logradouro
''',''' as b,
CASE WHEN CLI_NUMERO = '' THEN 'S/N' ELSE CLI_NUMERO END as CLI_NUMERO,--cen_numero
''',''' as c,
ISNULL(CLI_CEP,''),--cen_cep
''',''' as d,
ISNULL(CLI_OBSENDERECO,''),--cen_complemento
''',' as e,
ISNULL(BAI_CODIGO,198),--bai_codigo
',' as f,
CLI_CIDADE,--cid_codigo
',' as g,
CLI_ESTADO,--est_codigo
',' as h,
CLI_CODIGO,--cli_codigo
')' as i
from 
(select
--cen_codigo
[CLI_ENDERECO] as [CLI_ENDERECOOLD],--cen_logradouro
dbo.funEndereco([CLI_ENDERECO],'L') as CLI_ENDERECO,--cen_logradouro
dbo.funEndereco([CLI_ENDERECO],'N') as CLI_NUMERO,--cen_numero
[CLI_CEP],--cen_cep
[CLI_OBSENDERECO],--cen_complemento
(select [bai_codigoNovo] from @tbBairroNovo where [bai_codigo] = CLIENTES.BAI_CODIGO) as BAI_CODIGO,--bai_codigo
[BAI_CODIGO] AS [BAI_CODIGOOLD],--bai_codigo
1 as CLI_CIDADE,--cid_codigo
25 AS CLI_ESTADO,--est_codigo
(select [cli_codigoNovo] from @tbClienteNovo where [cli_codigo] = CLIENTES.CLI_CODIGO) as CLI_CODIGO,--cli_codigo
[CLI_CODIGO] AS [CLI_CODIGOOLD]--cli_codigo
from CLIENTES 
where CLI_ENDERECO IS NOT NULL AND CLI_ENDERECO <> ''
union
select
--cen_codigo
[CLI_ENDERECO2] as [CLI_ENDERECOOLD],--cen_logradouro
dbo.funEndereco([CLI_ENDERECO2],'L') as CLI_ENDERECO,--cen_logradouro
dbo.funEndereco([CLI_ENDERECO2],'N') AS CLI_NUMERO,--cen_numero
'' as [CLI_CEP],--cen_cep
'' as [CLI_OBSENDERECO],--cen_complemento
(select [bai_codigoNovo] from @tbBairroNovo where [bai_codigo] = CLIENTES.BAI_CODIGO2) as BAI_CODIGO,--bai_codigo
[BAI_CODIGO2] AS [BAI_CODIGOOLD],--bai_codigo
1 as CLI_CIDADE,--cid_codigo
25 AS CLI_ESTADO,--est_codigo
(select [cli_codigoNovo] from @tbClienteNovo where [cli_codigo] = CLIENTES.CLI_CODIGO) as CLI_CODIGO,--cli_codigo
[CLI_CODIGO] AS [CLI_CODIGOOLD]--cli_codigo
from CLIENTES 
where CLI_ENDERECO2 IS NOT NULL AND CLI_ENDERECO2 <> ''
union
select
--cen_codigo
[CLI_ENDERECO3] as [CLI_ENDERECOOLD],--cen_logradouro
dbo.funEndereco([CLI_ENDERECO3],'L') as CLI_ENDERECO,--cen_logradouro
dbo.funEndereco([CLI_ENDERECO3],'N') AS CLI_NUMERO,--cen_numero
'' as [CLI_CEP],--cen_cep
'' as [CLI_OBSENDERECO],--cen_complemento
(select [bai_codigoNovo] from @tbBairroNovo where [bai_codigo] = CLIENTES.BAI_CODIGO3) as BAI_CODIGO,--bai_codigo
[BAI_CODIGO3] AS [BAI_CODIGOOLD],--bai_codigo
1 as CLI_CIDADE,--cid_codigo
25 AS CLI_ESTADO,--est_codigo
(select [cli_codigoNovo] from @tbClienteNovo where [cli_codigo] = CLIENTES.CLI_CODIGO) as CLI_CODIGO,--cli_codigo
[CLI_CODIGO] AS [CLI_CODIGOOLD]--cli_codigo
from CLIENTES 
where CLI_ENDERECO3 IS NOT NULL AND CLI_ENDERECO3 <> '') as tb
order by tb.CLI_CODIGO