
namespace Symber.Web.Report
{

	internal enum RpnOperator
	{
		AND,
		OR
	}


	internal abstract class RptUnit
	{

		public abstract string GetDef();

	}


	internal interface IRightRpnUnit
	{

		void SetRight(RptUnit right);


		bool IsCompleted { get; }

	}


	internal class BinaryRpnUnit : RptUnit, IRightRpnUnit
	{

		#region [ Fields ]


		private RptUnit _left;
		private RptUnit _right;
		private RpnOperator _op;


		#endregion


		#region [ Constructors ]


		public BinaryRpnUnit(RptUnit left, RpnOperator op)
		{
			_left = left;
			_op = op;
		}


		#endregion


		#region [ Properties ]


		public RptUnit Left
		{
			get { return _left; }
			set { _left = value; }
		}


		public RptUnit Right
		{
			get { return _right; }
		}


		public RpnOperator Op
		{
			get { return _op; }
			set { _op = value; }
		}


		public bool IsCompleted
		{
			get
			{
				if (_right is IRightRpnUnit)
					return (_right as IRightRpnUnit).IsCompleted;
				return _right != null;
			}
		}


		#endregion


		#region [ Methods ]


		public override string GetDef()
		{
			return _left.GetDef() + _right.GetDef() + (_op == RpnOperator.AND ? " &" : " |");
		}


		public void SetRight(RptUnit right)
		{
			if (_right is IRightRpnUnit)
				(_right as IRightRpnUnit).SetRight(right);
			else
				_right = right;
		}


		#endregion

	}


	internal class UnaryNotRpnUnit : RptUnit, IRightRpnUnit
	{

		#region [ Fields ]


		private RptUnit _right;


		#endregion


		#region [ Constructors ]


		public UnaryNotRpnUnit()
		{
		}


		#endregion


		#region [ Properties ]


		public RptUnit Right
		{
			get { return _right; }
		}


		public bool IsCompleted
		{
			get
			{
				if (_right is IRightRpnUnit)
					return (_right as IRightRpnUnit).IsCompleted;
				return _right != null;
			}
		}


		#endregion


		#region [ Methods ]


		public override string GetDef()
		{
			return _right.GetDef() + " !";
		}


		public void SetRight(RptUnit right)
		{
			if (_right is IRightRpnUnit)
				(_right as IRightRpnUnit).SetRight(right);
			else
				_right = right;
		}


		#endregion

	}


	internal class OperandRpnUnit : RptUnit
	{

		#region [ Fields ]


		private string _identifier;


		#endregion


		#region [ Constructors ]


		public OperandRpnUnit(string identifier)
		{
			_identifier = identifier;
		}


		#endregion


		#region [ Properties ]


		public string Identifier
		{
			get { return _identifier; }
			set { _identifier = value; }
		}


		#endregion


		#region [ Methods ]


		public override string GetDef()
		{
			return " " + _identifier;
		}


		#endregion

	}

}
