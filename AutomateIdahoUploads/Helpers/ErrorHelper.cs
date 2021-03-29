using CommonCode.Helpers;
using System;

namespace AutomateIdahoUploads.Helpers
{
    public class ErrorHelper : IErrorHelper
    {
        public bool CourseNumberNotFound(string text)
        {
            throw new NotImplementedException();
        }

        public bool CourseOutOfDateRange(string text)
        {
            throw new NotImplementedException();
        }

        public bool HasAlreadyUsedCourse(string text)
        {
            throw new NotImplementedException();
        }

        public bool HasInvalidLicense(string text)
        {
            throw new NotImplementedException();
        }

        public bool LienseAlreadyOnRoster(string text)
        {
            throw new NotImplementedException();
        }
    }
}
