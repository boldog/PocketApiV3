using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace PocketApiV3
{
    enum ApiRequestContentType
    {
        JsonBody,
        FormUrlEncoded
    }

    public enum PocketItemStatus
    {
        Normal = 0,
        Archived = 1,
        Deleted = 2
    }

    public enum PocketVideoType
    {
        Undefined = 0,
        YouTube = 1,
        Vimeo = 2,
        Html = 5,
        Flash = 6,
        //Unknown = 7,
    }

    public enum RetrieveContentType
    {
        [EnumMember(Value = "article")]
        Article,

        [EnumMember(Value = "video")]
        Video,

        [EnumMember(Value = "image")]
        Image
    }

    public enum RetrieveDetailType
    {
        /// <summary>
        /// Includes all data except for child objects, like Tags, Authors,
        /// Images, Videos, etc.
        /// </summary>
        [EnumMember(Value = "simple")]
        Simple,

        /// <summary>
        /// Includes all data and child objects.
        /// </summary>
        [EnumMember(Value = "complete")]
        Complete
    }

    public enum RetrieveSort
    {
        [EnumMember(Value = "newest")]
        NewestFirst,

        [EnumMember(Value = "oldest")]
        OldestFirst,

        /// <summary>
        /// Title A-Z descending
        /// </summary>
        // TODO rename to clarify effect
        [EnumMember(Value = "title")]
        Title,

        /// <summary>
        /// URL descending
        /// </summary>
        // TODO rename to clarify effect
        [EnumMember(Value = "site")]
        Site
    }

    /// <summary>
    /// Item states
    /// </summary>
    public enum RetrieveState
    {
        /// <summary>
        /// Only unread items
        /// </summary>
        [EnumMember(Value = "unread")]
        Unread,

        /// <summary>
        /// Only archived items
        /// </summary>
        [EnumMember(Value = "archive")]
        Archive,

        /// <summary>
        /// All items
        /// </summary>
        [EnumMember(Value = "all")]
        All
    }

    public enum SubsumptionRelationship
    {
        None = 0,

        /// <summary>
        /// Has-a relationship.  The item has/contains one or more of x.
        /// </summary>
        Has = 1,

        /// <summary>
        /// Is-a relationship.  The item is a x.
        /// </summary>
        Is = 2
    }

}
