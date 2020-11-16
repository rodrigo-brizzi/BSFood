select 
'insert into tbBairro values (''' as a,
[BAI_NOME],--bai_nome
''',' as b,
[BAI_TAXA_ENTREGA],--bai_taxaEntrega
',' as c,
CASE WHEN [BAI_ENTREGA] = 'S' THEN 1 ELSE 0 END as [BAI_ENTREGA],--bai_realizaEntrega
') --' as d,
ROW_NUMBER() OVER (ORDER BY [BAI_CODIGO])  AS Codigo_novo
from BAIRROS 
order by [BAI_CODIGO]