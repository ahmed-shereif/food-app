namespace Domain.Enums
{
    public enum ErrorCodeEnum
    {
        None =0,
        AlreadyExist = 100,
        FailerUpdated = 101,
        FailerDelete = 102,
        Success=200,
        RoomAlreadyExist = 100,
        FailerUpdatedRoom = 101,
        FailerDeleteRoom = 102,
        NotFoundRoom = 103,
        NoRoomAvailable = 104,
        CheckInDateMustBeLessThanCheckOutDate = 105,
        InternalServerError =500,
        NotFound = 404,
        BadRequest = 400,
        BusinessRuleViolation=700,

      

        NotFoundReservation = 200,
        FailercancelReservation = 701,

        failerCreatePayment = 800,
    }
}
