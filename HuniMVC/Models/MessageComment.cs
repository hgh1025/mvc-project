
using System;

namespace HuniMVC.Models
{
    public class MessageComment
    {


            public virtual Message Message { get; set; }
        //public string Writer { get; set; }
        public Guid Id { get; set; }
        /// <summary>
        /// 작성자
        /// </summary>
        //public virtual UserValueObject Writer
        //{
        //    get { return _writer ?? (_writer = new UserValueObject()); }
        //    set
        //    {
        //        Guard.Assert(Writer != null, "Writer should not be null.");
        //        _writer = value;
        //    }
        //}

        ///// <summary>
        /// 이모테이션
        /// </summary>

            /// <summary>
            /// 본문
            /// </summary>
            public virtual string Body { get; set; }


            /// <summary>
            /// 등록일
            /// </summary>
            public virtual DateTime? CreateDate { get; set; }

        }
    }


