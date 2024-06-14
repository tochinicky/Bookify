using System.Data;
using Dapper;

namespace Bookify.Infrastructure;

// this is related to dapper, i need to tell dapper how to be able map the type,
//because it doesn't support it out of the box
internal sealed class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override DateOnly Parse(object value) => DateOnly.FromDateTime((DateTime)value);

    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.DbType = DbType.Date;
        parameter.Value = value;
    }
}
