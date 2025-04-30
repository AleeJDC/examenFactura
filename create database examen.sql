create database examen


USE examen

create table usuario(
IdUser int primary key identity(1,1),
email varchar(100),
password varchar(500),
active int
)


create database examen


USE examen

create procedure validar_usuario(
@email varchar(100),
@password varchar(500)
)
as 
begin
	if(exists(select * from usuario where email = @email and password = @password))
		select IdUser from usuario where email = @email and password = @password
	else
		select '0' as IdUser
end


insert into usuario (email, password) values ('alejandra@gmail.com', '239af32f3e2e134b080dfe125d0c11f6a71f6c233d3bd833ac6f769e45641d97')