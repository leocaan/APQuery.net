using System;

namespace Symber.Web
{
	internal static class APCVHelper
	{

		// Some APConverters

		internal static CommaDelimitedStringCollectionAPConverter CommaDelimitedStringCollectionConverter = new CommaDelimitedStringCollectionAPConverter();
		internal static DefaultAPValidator DefaultValidator = new DefaultAPValidator();
		internal static InfiniteTimeSpanAPConverter InfiniteTimeSpanConverter = new InfiniteTimeSpanAPConverter();
		internal static InfiniteIntAPConverter InfiniteIntConverter = new InfiniteIntAPConverter();
		internal static TimeSpanMinutesAPConverter TimeSpanMinutesConverter = new TimeSpanMinutesAPConverter();
		internal static TimeSpanMinutesOrInfiniteAPConverter TimeSpanMinutesOrInfiniteConverter = new TimeSpanMinutesOrInfiniteAPConverter();
		internal static TimeSpanSecondsAPConverter TimeSpanSecondsConverter = new TimeSpanSecondsAPConverter();
		internal static TimeSpanSecondsOrInfiniteAPConverter TimeSpanSecondsOrInfiniteConverter = new TimeSpanSecondsOrInfiniteAPConverter();
		internal static WhiteSpaceTrimStringAPConverter WhiteSpaceTrimStringConverter = new WhiteSpaceTrimStringAPConverter();


		// Some APValidators

		internal static NullableStringAPValidator NonEmptyStringValidator = new NullableStringAPValidator(1);
		internal static IntegerAPValidator IntFromZeroToMaxValidator = new IntegerAPValidator(0, Int32.MaxValue);
		internal static IntegerAPValidator IntFromOneToMax_1Validator = new IntegerAPValidator(1, Int32.MaxValue - 1);
		internal static PositiveTimeSpanAPValidator PositiveTimeSpanValidator = new PositiveTimeSpanAPValidator();
		internal static PropertyNameAPValidator PropertyNameValidator = new PropertyNameAPValidator();

	}
}
