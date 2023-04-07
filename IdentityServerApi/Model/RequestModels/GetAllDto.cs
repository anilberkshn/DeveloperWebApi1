namespace IdentityServerApi.Model.RequestModels
{
    public class GetAllDto
    {
        public int skip { get; set; }

        public int take { get; set; }

        public GetAllDto(int skip, int take)
        {
            this.skip = skip;
            this.take = take;
        }

        public GetAllDto()
        {
                
        }
    }
}