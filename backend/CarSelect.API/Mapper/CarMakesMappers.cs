public static class CarMakesMapper
{
    public static CarMakesResponseContract MapToContract(this CarMakesModel carMakesModel)
    {
        return new CarMakesResponseContract
        {
            MakeId = carMakesModel.Make_ID,
            MakeName = carMakesModel.Make_Name
        };
    }

}