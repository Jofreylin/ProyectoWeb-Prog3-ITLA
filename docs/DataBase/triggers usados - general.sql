create trigger tgr_insert_categoriasUsadas on [dbo].[POST]
after insert
as
begin
    declare @sumatorio int
    declare @anterior int
    declare @idPostCate int

    select @idPostCate = Nombre_Categoria from inserted
    select @anterior = Cantidad from [dbo].[CATEGORIA] where ID = @idPostCate
    
    if @anterior = null
    begin
        set @anterior = 0
    end

    if @anterior < 0
    begin
        set @anterior = 0
    end

    set @sumatorio = @anterior +1

    update [dbo].[CATEGORIA] set Cantidad = @sumatorio where id=@idPostCate

end

create trigger tgr_delete_categoriasUsadas on [dbo].[POST]
after delete
as
begin
    declare @resta int;
    declare @anterior int;
    declare @idPostCate int;

    select @idPostCate = Nombre_Categoria from deleted
    select @anterior = Cantidad from [dbo].[CATEGORIA] where ID = @idPostCate
    
    if @anterior = null
    begin
        set @anterior = 1
    end

    if @anterior < 1
    begin
        set @anterior = 1
    end

    set @resta = @anterior - 1

    update [dbo].[CATEGORIA] set Cantidad = @resta where id=@idPostCate
end