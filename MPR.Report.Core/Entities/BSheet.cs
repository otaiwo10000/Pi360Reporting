using MPR.Report.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPR.Report.Core.Entities
{
    public partial class BSheet : IIdentifiableEntity
    {
        int _BSheetId;
        string _Name;
        string _isDeleted;

        public int BSheetId
        {
            get
            {
                return _BSheetId;
            }
            set
            {
                //if (_BSheetId != value)
                //{
                //    _BSheetId = value;
                //}

                _BSheetId = value;
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                //if (_Name != value)
                //{
                //    _Name = value;
                //}

                _Name = value;
            }
        }

        public string isDeleted
        {
            get
            {
                return _isDeleted;
            }
            set
            {
                //    if (_isDeleted != value)
                //    {
                //        _isDeleted = value;
                //    }

                _isDeleted = value;
            }
        }

        public int EntityId
        {
            get
            {
                return BSheetId;
            }
        }


    }
}
