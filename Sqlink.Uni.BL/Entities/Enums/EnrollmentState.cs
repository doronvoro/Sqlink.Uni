namespace Sqlink.Uni.BL
{
    //    האפשרויות של המעברים ממצב למצב
    //InProgress → Completed.a
    //InProgress → Cancelled.b
    //Completed → InProgress.c
    //Completed → Cancelled.d
    //Completed → Payed
    public enum EnrollmentState
    {
        InProgress,
        Completed,
        Cancelled,
        Payed
    }


}
