using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dog.Models
{
    public class EnumList
    {
    }
    public enum Role
    {
        使用者 = 1,
        接單員 = 2,
    }
    public enum OrderStatus
    {
        待處理 = 1,
        進行中 = 2,
        已完成 = 3,
        已取消 = 4
    }
    public enum PaymentStatus
    {
        未付款 = 0,
        已付款 = 1,
        已退款 = 2
    }
    public enum IdentityEnum
    {
        操作員 = 1,
        主管 = 2,
        老闆 = 3
    }
}