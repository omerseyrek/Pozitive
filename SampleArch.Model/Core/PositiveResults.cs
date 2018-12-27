using SampleArch.Model.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Service.Core
{

    public class PositiveResults
    {
        public bool Success { get; set; }

        public List<ValidationResult> Messages { get; set; }

        public void AddResultsRange(List<ValidationResult> toAdd)
        {
            if (Messages == null) Messages = new List<ValidationResult>();

            Messages.AddRange(toAdd);
        }
    }

    public class ValidationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class.
        /// </summary>
        public ValidationResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class.
        /// </summary>
        /// <param name="memeberName">Name of the memeber.</param>
        /// <param name="message">The message.</param>
        public ValidationResult(MessageType mType, string memeberName, string message)
        {
            this.MessType = mType;
            this.MemberName = memeberName;
            this.Message = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResult"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ValidationResult(string message)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets or sets the name of the member.
        /// </summary>
        /// <value>
        /// The name of the member.  May be null for general validation issues.
        /// </value>
        public string MemberName { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }



        public MessageType MessType { get; set; }
    }
}
