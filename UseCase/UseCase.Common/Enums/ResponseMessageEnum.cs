using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UseCase.Common.Enums
{
    public enum ResponseMessageEnum
    {
        [Description("Başarılı")]
        Success,
        [Description("Bulunamadı")]
        NotFound,
        [Description("Hata")]
        Exception,
        [Description("Yetkisiz işlem")]
        UnAuthorized,
        [Description("Kullanıcı Eklendi")]
        UserAttached,
        [Description("Ödenmemiş fatura var.")]
        InvoiceStatusError,
        [Description("Ödenmemiş depozito var.")]
        UserDepositError,
        [Description("Abone bulunamadı.")]
        SubscriptionNotFound,
        [Description("Abone daha önce eklenmiş.")]
        UserIsAttached

        
        

    }
}
