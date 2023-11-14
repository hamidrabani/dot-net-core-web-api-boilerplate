using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.OpenIddict;

namespace boilerplate.GlobalSettings
{
    public class GlobalSettingAppService :
        boilerplateAppService, IGlobalSettingAppService

    {
        private readonly IRepository<GlobalSetting, long> _globalsettingRepository;

        public GlobalSettingAppService(IRepository<GlobalSetting, long> globalsettingRepository)
        {
            _globalsettingRepository = globalsettingRepository;
        }

        public async Task CreateAsync(CreateUpdateGlobalSettingDto input)
        {
            await _globalsettingRepository.InsertAsync(
                ObjectMapper.Map<CreateUpdateGlobalSettingDto, GlobalSetting>(input));
            //throw new System.NotImplementedException();
        }

        public async Task<GlobalSettingDto> GetAsync(long id)
        {
            var gs = await _globalsettingRepository.GetAsync(id);
            return ObjectMapper.Map<GlobalSetting, GlobalSettingDto>(gs);
            //throw new System.NotImplementedException();
        }

        public async Task<GlobalSettingDto> GetByKeyNameAsync(string keyName)
        {
            var gs = await _globalsettingRepository.GetAsync(c=>c.key_name == keyName);
            return ObjectMapper.Map<GlobalSetting, GlobalSettingDto>(gs);
            //throw new System.NotImplementedException();
        }

        public async Task<PagedResultDto<GlobalSettingDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            //var query = await _globalsettingRepository.GetListAsync();
            var query = await _globalsettingRepository.WithDetailsAsync();

            query = query.Skip(input.SkipCount)
                         .Take(input.MaxResultCount);
                         //.OrderBy(input.Sorting ?? nameof(GlobalSetting.group_name));
            var gs = await AsyncExecuter.ToListAsync(query);
            var count = await _globalsettingRepository.GetCountAsync();

            return new PagedResultDto<GlobalSettingDto>(
                count,
                ObjectMapper.Map<List<GlobalSetting>, List<GlobalSettingDto>>
                (gs)
            );

            //throw new System.NotImplementedException();
        }

        public async Task UpdaeteAsync(long id, CreateUpdateGlobalSettingDto input)
        {
            var gs = await _globalsettingRepository.GetAsync(id);
            ObjectMapper.Map(input, gs);
            // await _globalsettingRepository.UpdateAsync(
            //     ObjectMapper.Map<CreateUpdateGlobalSettingDto, GlobalSetting>(input));
            //throw new System.NotImplementedException();
        }
    }
}