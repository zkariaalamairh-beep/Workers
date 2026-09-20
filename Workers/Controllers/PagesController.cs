using Microsoft.AspNetCore.Mvc;
using Workers.ViewModels;

namespace Workers.Controllers;

public class PagesController : Controller
{
    [HttpGet]
    public IActionResult About()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Faq()
    {
        var model = new FaqPageViewModel
        {
            Categories =
            [
                new FaqCategoryViewModel
                {
                    Title = "الحساب واستخدام المنصة",
                    Icon = "👤",
                    Questions =
                    [
                        new FaqItemViewModel
                        {
                            Id = "faq-account-1",
                            Question = "هل أحتاج إلى حساب لتصفح الخدمات؟",
                            Answer =
                                "لا، يمكنك تصفح مقدمي الخدمات وعمال اليومية دون تسجيل. لكنك تحتاج إلى حساب عند إرسال طلب خدمة أو متابعة طلباتك."
                        },
                        new FaqItemViewModel
                        {
                            Id = "faq-account-2",
                            Question = "هل يمكن للشخص أن يكون عميلًا ومقدم خدمة؟",
                            Answer =
                                "نعم، الحساب نفسه يستطيع طلب الخدمات، ويمكن لصاحبه إنشاء ملف مقدم خدمة وعرض مهاراته وخدماته."
                        },
                        new FaqItemViewModel
                        {
                            Id = "faq-account-3",
                            Question = "ماذا أفعل إذا نسيت كلمة المرور؟",
                            Answer =
                                "سنضيف خدمة استعادة كلمة المرور عبر البريد الإلكتروني. وحتى تفعيلها يمكنك التواصل مع فريق الدعم."
                        }
                    ]
                },

                new FaqCategoryViewModel
                {
                    Title = "الموقع والخدمات القريبة",
                    Icon = "📍",
                    Questions =
                    [
                        new FaqItemViewModel
                        {
                            Id = "faq-location-1",
                            Question = "لماذا يطلب الموقع الوصول إلى موقعي؟",
                            Answer =
                                "يُستخدم موقعك لعرض مقدمي الخدمات الأقرب إليك وترتيب النتائج حسب المسافة. لن يتم الوصول إليه دون موافقتك."
                        },
                        new FaqItemViewModel
                        {
                            Id = "faq-location-2",
                            Question = "هل يجب أن أسمح بالوصول إلى الموقع؟",
                            Answer =
                                "لا، السماح اختياري بالكامل. يمكنك البحث عن مقدمي الخدمات بالطريقة العادية دون استخدام موقعك."
                        },
                        new FaqItemViewModel
                        {
                            Id = "faq-location-3",
                            Question = "لماذا لا تظهر خدمات قريبة مني؟",
                            Answer =
                                "قد لا يوجد مقدم خدمة متاح ضمن المسافة المحددة. جرّب زيادة نطاق البحث أو عرض جميع مقدمي الخدمات."
                        }
                    ]
                },

                new FaqCategoryViewModel
                {
                    Title = "طلبات الخدمات",
                    Icon = "🧰",
                    Questions =
                    [
                        new FaqItemViewModel
                        {
                            Id = "faq-request-1",
                            Question = "كيف أطلب خدمة؟",
                            Answer =
                                "افتح ملف مقدم الخدمة، اختر الخدمة المناسبة، ثم اضغط على طلب الخدمة وحدد الوصف والموعد ومكان تنفيذ العمل."
                        },
                        new FaqItemViewModel
                        {
                            Id = "faq-request-2",
                            Question = "هل يمكنني إلغاء الطلب؟",
                            Answer =
                                "يمكن إلغاء الطلب قبل بدء العمل مع كتابة سبب الإلغاء. بعد بدء التنفيذ يُفضّل التواصل مع مقدم الخدمة أولًا."
                        },
                        new FaqItemViewModel
                        {
                            Id = "faq-request-3",
                            Question = "كيف أعرف حالة طلبي؟",
                            Answer =
                                "ستظهر حالة الطلب داخل صفحة طلباتي، مثل قيد الانتظار، مقبول، قيد التنفيذ، مكتمل أو ملغي."
                        }
                    ]
                },

                new FaqCategoryViewModel
                {
                    Title = "الأسعار والتقييمات",
                    Icon = "★",
                    Questions =
                    [
                        new FaqItemViewModel
                        {
                            Id = "faq-review-1",
                            Question = "كيف يتم تحديد سعر الخدمة؟",
                            Answer =
                                "قد يكون السعر ثابتًا أو بالساعة أو باليوم أو قابلًا للتفاوض، حسب الخدمة التي أضافها مقدم الخدمة."
                        },
                        new FaqItemViewModel
                        {
                            Id = "faq-review-2",
                            Question = "متى يمكنني تقييم مقدم الخدمة؟",
                            Answer =
                                "يمكن إضافة التقييم بعد اكتمال الطلب فقط، وذلك للمساعدة في جعل التقييمات مرتبطة بخدمات حقيقية."
                        },
                        new FaqItemViewModel
                        {
                            Id = "faq-review-3",
                            Question = "هل توفر المنصة الدفع الإلكتروني؟",
                            Answer =
                                "الدفع الإلكتروني غير متوفر حاليًا، ويتم الاتفاق على طريقة الدفع بين العميل ومقدم الخدمة. يمكن إضافته في إصدار لاحق."
                        }
                    ]
                },

                new FaqCategoryViewModel
                {
                    Title = "الأمان والدعم",
                    Icon = "🛡",
                    Questions =
                    [
                        new FaqItemViewModel
                        {
                            Id = "faq-support-1",
                            Question = "كيف أبلغ عن مشكلة أو حساب؟",
                            Answer =
                                "يمكنك فتح صفحة الدعم والمساعدة أو صفحة تواصل معنا وإرسال تفاصيل المشكلة ليتم التعامل معها."
                        },
                        new FaqItemViewModel
                        {
                            Id = "faq-support-2",
                            Question = "ماذا يعني حساب موثّق؟",
                            Answer =
                                "يعني أن إدارة المنصة راجعت معلومات مقدم الخدمة وفق آلية التحقق المعتمدة. تظهر علامة التوثيق بوضوح بجانب الحساب."
                        }
                    ]
                }
            ]
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Support()
    {
        return OpenPage(
            "؟",
            "الدعم والمساعدة",
            "نحن هنا لمساعدتك في استخدام الموقع وحل أي مشكلة تواجهك.");
    }

    [HttpGet]
    public IActionResult Blog()
    {
        return OpenPage(
            "✎",
            "المدونة",
            "مقالات ونصائح تساعد العملاء ومقدمي الخدمات وعمال اليومية.");
    }

    [HttpGet]
    public IActionResult Contact()
    {
        return OpenPage(
            "☎",
            "تواصل معنا",
            "أرسل لنا استفسارك أو اقتراحك أو أبلغنا عن مشكلة.");
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Terms()
    {
        return View();
    }
    private ViewResult OpenPage(
        string icon,
        string title,
        string description)
    {
        return View("Info", new InfoPageViewModel
        {
            Icon = icon,
            Title = title,
            Description = description
        });
    }
}