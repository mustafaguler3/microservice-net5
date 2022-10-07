using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Model;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            //veritabanı bağlantısı
            var client = new MongoClient(databaseSettings.ConnectionString);
            //veritabanı
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>>GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();

            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories),200);
        }

        public async Task<Response<CategoryDto>> CreateAsync(CategoryDto dto)
        {
            var categoryDto = _mapper.Map<Category>(dto);

            await _categoryCollection.InsertOneAsync(categoryDto);

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(categoryDto),200);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(i => i.Id == id).FirstOrDefaultAsync();
            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found",404);
            }
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category),200);
        }

    }
}
