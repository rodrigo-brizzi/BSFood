delete from tbClienteTelefone where cli_codigo in 
(
select cli_codigo from tbCliente where cli_codigo not in (select distinct cli_codigo from tbClienteEndereco)
)
delete from tbCliente where cli_codigo in 
(
select cli_codigo from tbCliente where cli_codigo not in (select distinct cli_codigo from tbClienteEndereco)
)