create database BDVeterinaria;
use BDVeterinaria;

create table tbTipoUsuario(
codTipoUsuario int primary key auto_increment,
usuario varchar(50)
);
insert into tbTipoUsuario values(default, "admin");
insert into tbTipoUsuario values(default, "usuário");


create table tbLogin(
usuario varchar(50) primary key,
senha varchar(10),
codTipoUsuario int,
foreign key (codTipoUsuario) references tbTipoUsuario(codTipoUsuario)
);
insert into tbLogin values("Console", "123456", 1);
insert into tbLogin values("Valen", "123456", 2);
select * from tbLogin;

create table tbCliente(
codCliente int primary key auto_increment,
nomeCliente varchar(50),
telefoneCliente varchar(12),
emailCliente varchar(50)
);

create table tipoAnimal(
codTipoAnimal int primary key auto_increment,
tipoAnimal varchar(30)
);

create table tbAnimal(
codAnimal int primary key auto_increment,
nomeAnimal varchar(50),
codTipoAnimal int,
codCliente int,
foreign key (codTipoAnimal) references tipoAnimal(codTipoAnimal),
foreign key (codCliente) references tbCliente(codCliente)
);

create table tbVeterinario(
codVet int primary key auto_increment,
nomeVet varchar(50)
);

create table tbAtendimento(
codAtendimento int primary key auto_increment,
DataAtendimento varchar(20),
HoraAtendimento varchar(8),
codAnimal int,
codVet int,
Diag varchar(50)
);