namespace ComputerShop.Models
{
	public class Paginate
	{
		public int TotalItems { get;private set; } //tong so items
		public int PageSize { get;private set; }//tong so item/trang
		public int CurrentPage { get;private set; }//trang hien tai
		public int TotalPages { get;private set; }//tong cung
		public int StartPage { get;private set; }//trang bat dau
		public int EndPage { get;private set; }//trang ket thuc
		public Paginate()
		{

		}
		public Paginate(int totalItems, int page , int pageSize =10)//10 items/trang
		{
			//lam tron tong iteams/10 items tren 1 trang VD:16 item/10= tron 3 trang
			int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);

			int currentPage = page;//page hien tai

			int startPage = currentPage - 5;//trang bat dau tru 5 buton

			int endPage = currentPage + 4; //trang cuoi cong them 4 buton
			if(startPage <= 0)
			{
				//neu so trang bat dau nho hon or = 0 thhi so trang cuoi se bang
				endPage = endPage - (startPage - 1);//6-(-3-1)=10
				startPage = 1;

			}
			if (endPage > totalPages)
			{
				endPage = totalPages;
				if(endPage > 10)
				{
					startPage = endPage - 9;
				}

            }
			TotalItems = totalItems;
			CurrentPage = currentPage;
			PageSize = pageSize;
			TotalPages = totalPages;
			StartPage = startPage;
			EndPage= endPage;

		}
	}
}
